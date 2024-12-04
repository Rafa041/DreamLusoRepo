import { Component, OnDestroy, OnInit } from '@angular/core';
import { Chat, ChatMessage, ChatStatus, MessageType } from '../../../../../../models/Chat';
import { Subject } from 'rxjs/internal/Subject';
import { ChatService } from '../../../../../../services/ChatService/chat.service';
import { interval } from 'rxjs/internal/observable/interval';
import { takeUntil } from 'rxjs/internal/operators/takeUntil';
import { switchMap } from 'rxjs/internal/operators/switchMap';
import { Property } from '../../../../../../models/property';
import { UserModel } from '../../../../../../models/UserModel';
import { PropertyService } from '../../../../../../services/PropertyService/property.service';
import { UserService } from '../../../../../../services/UserService/user.service';
import { RealStateAgentService } from '../../../../../../services/RealStateAgent/real-state-agent.service';
import { RealStateAgentModel } from '../../../../../../models/RealStateAgentModel';
import { environment } from '../../../../../../../../environment';

@Component({
  selector: 'app-chat-agent',
  templateUrl: './chat-agent.component.html',
  styleUrl: './chat-agent.component.scss'
})
export class ChatAgentComponent implements OnInit, OnDestroy {
  activeChats: (Chat & { propertyDetails?: Property; loggedUserDetails?: UserModel })[] = [];
  messages: ChatMessage[] = [];
  selectedChat: (Chat & { propertyDetails?: Property; loggedUserDetails?: UserModel }) | null = null;
  newMessage: string = '';
  agentId: string = '';
  loggedUserDetails: UserModel | null = null;
  private destroy$ = new Subject<void>();
  private readonly POLLING_INTERVAL = 10000;
  apiUrl = environment.apiUrl;

  quickResponses: string[] = [
    'I ll check the property availability right away.',
    'Would you like to schedule a viewing?',
    'I can offer you more details about this property.',
    'Let me send you the complete documentation.'
  ];

  constructor(
    private chatService: ChatService,
    private propertyService: PropertyService,
    private userService: UserService,
    private realStateAgentService: RealStateAgentService
  ) {}

  ngOnInit(): void {
    this.loadLoggedUserDetails();
  }

  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }

  private loadLoggedUserDetails(): void {
    const loggedUserString = sessionStorage.getItem('loggedUser');
    if (loggedUserString) {
      const loggedUser = JSON.parse(loggedUserString);
      this.userService.retrieve(loggedUser.id).subscribe({
        next: (userDetails: UserModel) => {
          this.loggedUserDetails = userDetails;
          this.realStateAgentService.retrieveByUserId(loggedUser.id).subscribe({
            next: (agentDetails: RealStateAgentModel) => {
              this.agentId = agentDetails.id;
              this.loadActiveChats();
              this.setupChatPolling();
            },
            error: (error) => {
              console.error('Error fetching agent details:', error);
            }
          });
        },
        error: (error) => {
          console.error('Error fetching user details:', error);
        }
      });
    }
  }

  private setupChatPolling(): void {
    interval(this.POLLING_INTERVAL)
      .pipe(
        takeUntil(this.destroy$),
        switchMap(() => this.chatService.getAgentChats(this.agentId))
      )
      .subscribe({
        next: (chats) => {
          this.enrichChatsWithDetails(chats);
          if (this.selectedChat) {
            this.loadMessages(this.selectedChat.id);
          }
        },
        error: (error) => {
          console.error('Error in chat polling:', error);
        }
      });
  }

  private enrichChatsWithDetails(chats: Chat[]): void {
    const enrichedChats = chats.map(chat => {
      const enrichedChat = { ...chat } as Chat & { propertyDetails?: Property; loggedUserDetails?: UserModel };

      this.propertyService.retrieve(chat.propertyId).subscribe({
        next: (property) => {
          enrichedChat.propertyDetails = property;
        },
        error: (error) => {
          console.error('Error fetching property details:', error);
        }
      });

      this.userService.retrieve(chat.userId).subscribe({
        next: (user) => {
          enrichedChat.loggedUserDetails = user;
        },
        error: (error) => {
          console.error('Error fetching user details:', error);
        }
      });

      return enrichedChat;
    });

    this.activeChats = enrichedChats;
  }

  loadActiveChats(): void {
    if (!this.agentId) return;

    this.chatService.getAgentChats(this.agentId).subscribe({
      next: (chats) => {
        console.log('Received chats:', chats);
        this.enrichChatsWithDetails(chats);
      },
      error: (error) => {
        console.error('Error loading chats:', error);
      }
    });
  }

  selectChat(chat: Chat & { propertyDetails?: Property; loggedUserDetails?: UserModel }): void {
    this.selectedChat = chat;
    this.loadMessages(chat.id);
  }

  loadMessages(chatId: string): void {
    this.chatService.getChatMessages(chatId).subscribe({
        next: (messages) => {
            this.messages = messages
                .map(message => ({
                    ...message,
                    isCurrentUserMessage: message.senderId === this.loggedUserDetails?.id
                }))
                .sort((a, b) => new Date(a.sentAt).getTime() - new Date(b.sentAt).getTime());
        },
        error: (error) => {
            console.error('Error loading messages:', error);
        }
    });
  }

  sendMessage(): void {
    if (!this.newMessage.trim() || !this.selectedChat || !this.loggedUserDetails?.id) return;

    const messageToSend = {
        chatId: this.selectedChat.id,
        senderId: this.loggedUserDetails.id,
        content: this.newMessage,
        type: MessageType.Text
    };

    this.chatService.createMessage(messageToSend).subscribe({
        next: (newMessage: ChatMessage) => {
            const messageWithFlag = {
                ...newMessage,
                isCurrentUserMessage: true
            };
            this.messages = [...this.messages, messageWithFlag].sort(
                (a, b) => new Date(a.sentAt).getTime() - new Date(b.sentAt).getTime()
            );
            this.newMessage = '';

            if (this.selectedChat) {
                this.selectedChat.lastMessageContent = newMessage.content;
                this.selectedChat.lastMessageAt = newMessage.sentAt;
            }
        },
        error: (error) => {
            console.error('Error sending message:', error);
        }
    });
  }


  private updateChatLastMessage(message: ChatMessage): void {
    if (this.selectedChat) {
      this.selectedChat.lastMessageContent = message.content;
      this.selectedChat.lastMessageAt = message.sentAt;
    }
  }

  useQuickResponse(response: string): void {
    this.newMessage = response;
  }

  getImageUrl(imageUrl: string | undefined): string {
    if (!imageUrl) {
      return 'assets/default-avatar.jpg';
    }
    return `${this.apiUrl}${imageUrl}`;
  }

  handleImageError(event: any): void {
    event.target.src = 'assets/images/default-avatar.png';
  }
}

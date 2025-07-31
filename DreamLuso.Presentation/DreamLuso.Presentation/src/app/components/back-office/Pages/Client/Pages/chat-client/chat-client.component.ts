import { Component, Input } from '@angular/core';
import { UserModel } from '../../../../../../models/UserModel';
import { UserService } from '../../../../../../services/UserService/user.service';
import { ChatService } from '../../../../../../services/ChatService/chat.service';
import { Router } from '@angular/router';
import { Chat, ChatMessage, ChatStatus, MessageType } from '../../../../../../models/Chat';
import { AuthService } from '../../../../../../services/AuthService/auth.service';
import { environment } from '../../../../../../../../environment';
import { RealEstateAgentService } from '../../../../../../services/RealEstateAgent/real-estate-agent.service';
import { RealEstateAgentModel } from '../../../../../../models/RealEstateAgentModel';
interface ChatWithAgent extends Chat {
  agentDetails?: RealEstateAgentModel;
}
@Component({
  selector: 'app-chat-client',
  templateUrl: './chat-client.component.html',
  styleUrl: './chat-client.component.scss'
})

export class ChatClientComponent {
  chat: Chat | null = null;
  messages: ChatMessage[] = [];
  newMessage: string = '';
  currentUser!: UserModel | null;
  loggedUserDetails: UserModel | null = null;
  activeChats: ChatWithAgent[] = [];
  selectedChat: ChatWithAgent | null = null;
  userId: string = '';
  private apiUrl = environment.apiUrl;



  constructor(
    private chatService: ChatService,
    private router: Router,
    private authService: AuthService,
    private realEstateAgentService: RealEstateAgentService
  ) {
    const loggedUserString = sessionStorage.getItem('loggedUser');
    if (loggedUserString) {
      this.currentUser = JSON.parse(loggedUserString);
      this.userId = this.currentUser?.id || '';
    }
    const navigation = this.router.getCurrentNavigation();
    this.chat = navigation?.extras?.state?.['chat'];
  }

  ngOnInit() {
    this.loadUserDetails();
    this.loadActiveChats();
    if (this.chat) {
      this.selectChat(this.chat);
    }
  }

  loadUserDetails() {
    this.loggedUserDetails = this.authService.getCurrentUser();
  }

  loadActiveChats() {
    const loggedUserString = sessionStorage.getItem('loggedUser');
    if (loggedUserString) {
        const loggedUser = JSON.parse(loggedUserString);
        this.userId = loggedUser.id;

        this.chatService.getUserChats(this.userId).subscribe({
            next: (chats) => {
                this.activeChats = chats as ChatWithAgent[];  // Cast to ChatWithAgent[]
                this.activeChats.forEach(chat => {
                    this.realEstateAgentService.retrieve(chat.realEstateAgentId).subscribe({
                        next: (agentDetails) => {
                            chat.agentDetails = agentDetails;
                        }
                    });
                });
            }
        });
    }
  }
  isActiveChat(chat: ChatWithAgent): boolean {
    return this.selectedChat?.id === chat.id;
  }

  selectChat(chat: ChatWithAgent) {
    this.selectedChat = chat;
    this.loadMessages(chat.id);
  }

  loadMessages(chatId: string) {
    this.chatService.getChatMessages(chatId).subscribe({
      next: (messages) => {
        // Sort messages by date ascending
        this.messages = messages.sort((a, b) =>
          new Date(a.sentAt).getTime() - new Date(b.sentAt).getTime()
        );
      },
      error: (error) => {
        console.error('Error loading messages:', error);
      }
    });
  }
  showDateSeparator(message: ChatMessage): boolean {
    const index = this.messages.indexOf(message);
    if (index === 0) return true;

    const previousMessage = this.messages[index - 1];
    const messageDate = new Date(message.sentAt).toDateString();
    const previousDate = new Date(previousMessage.sentAt).toDateString();

    return messageDate !== previousDate;
  }

  getImageUrl(imageUrl: string | undefined): string {
    if (!imageUrl) {
      return 'assets/default-avatar.jpg';
    }
    return `${this.apiUrl}${imageUrl}`;
  }

  handleImageError(event: any) {
    event.target.src = 'assets/default-avatar.jpg';
  }

  sendMessage() {
    if (!this.newMessage.trim() || !this.selectedChat || !this.currentUser) return;

    const messageToSend = {
      chatId: this.selectedChat.id,
      senderId: this.currentUser.id,
      content: this.newMessage,
      type: MessageType.Text
    };

    this.chatService.createMessage(messageToSend).subscribe({
      next: (newMessage: ChatMessage) => {
        this.messages = [...this.messages, newMessage];
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
}

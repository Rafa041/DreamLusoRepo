import { Injectable } from '@angular/core';
import { environment } from '../../../../environment';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs/internal/Observable';
import { Chat, ChatStatus, ChatMessage, MessageType, CreateChatDto } from '../../models/Chat';
import { tap } from 'rxjs/internal/operators/tap';
import { catchError } from 'rxjs/internal/operators/catchError';



@Injectable({
  providedIn: 'root'
})
export class ChatService {
  private apiUrl = `${environment.apiUrl}/api/chat`;
  private messageUrl = `${environment.apiUrl}/api/message`;

  constructor(private http: HttpClient) {}

  createChat(chat: any): Observable<Chat> {
    return this.http.post<Chat>(`${this.apiUrl}/create`, chat);
  }

  getAllChats(): Observable<Chat[]> {
    return this.http.get<Chat[]>(`${this.apiUrl}/retrieveall`);
  }

  getChatById(chatId: string): Observable<Chat> {
    return this.http.get<Chat>(`${this.apiUrl}/${chatId}`);
  }

  getUserChats(userId: string): Observable<Chat[]> {
    return this.http.get<Chat[]>(`${this.apiUrl}/user/${userId}`);
  }
  getAgentChats(agentId: string): Observable<Chat[]> {
    console.log('Service calling with agentId:', agentId); // Log in service
    return this.http.get<Chat[]>(`${this.apiUrl}/realstateagent/${agentId}`);
  }

  updateChatStatus(chatId: string, status: ChatStatus): Observable<boolean> {
    return this.http.patch<boolean>(`${this.apiUrl}/${chatId}/status`, { status });
  }

  // Message endpoints
  createMessage(message: Partial<ChatMessage>): Observable<ChatMessage> {
    const command = {
      chatId: message.chatId,
      senderId: message.senderId,
      content: message.content,
      type: message.type
    };

    return this.http.post<ChatMessage>(`${this.messageUrl}/create`, command).pipe(
      tap(response => console.log('Server response:', response)),
      catchError(error => {
        console.error('Server error:', error);
        throw error;
      })
    );
  }

  getChatMessages(chatId: string): Observable<ChatMessage[]> {
    return this.http.get<ChatMessage[]>(`${this.messageUrl}/chat/${chatId}`);
  }

  getUnreadMessages(userId: string): Observable<ChatMessage[]> {
    return this.http.get<ChatMessage[]>(`${this.messageUrl}/unread/${userId}`);
  }
  initiateChat(chatData: CreateChatDto): Observable<Chat> {
    return this.http.post<Chat>(`${this.apiUrl}/create`, chatData);
  }

  markMessageAsRead(messageId: string): Observable<boolean> {
    return this.http.patch<boolean>(`${this.messageUrl}/${messageId}/read`, {});
  }
}

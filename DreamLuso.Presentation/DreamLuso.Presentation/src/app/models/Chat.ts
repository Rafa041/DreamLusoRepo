export interface ChatMessage {
  id: string;
  chatId: string;
  senderId: string;
  senderName: string;
  senderImageUrl: string;
  content: string;
  sentAt: Date;
  isRead: boolean;
  type: MessageType;
  isCurrentUserMessage: boolean;
  readAt?: Date;
}

export enum MessageType {
  Text = 'Text',
  Image = 'Image',
  Document = 'Document'
}

export interface Chat {
  id: string;
  propertyId: string;
  propertyTitle: string;
  userId: string;
  userName: string;
  realEstateAgentId: string;
  realEstateAgentName: string;
  status: ChatStatus;
  lastMessageAt: Date;
  unreadMessagesCount: number;
  lastMessageContent: string;
}

export enum ChatStatus {
  Active = 'Active',
  Closed = 'Closed',
  Pending = 'Pending'
}
export interface CreateChatDto {
  propertyId: string;
  userId: string;
  realEstateAgentId: string;
}

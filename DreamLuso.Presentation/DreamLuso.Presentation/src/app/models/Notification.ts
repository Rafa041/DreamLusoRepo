export interface UINotification {
  message: string;
  type: 'success' | 'error';
}

export interface ApiNotification {
  id: string;
  message: string;
  createdAt: Date;
  type: string;
  priority: string;
  status: string;
  senderId: string;
  recipientId: string;
  senderName: string;
  expirationDate: Date;
}

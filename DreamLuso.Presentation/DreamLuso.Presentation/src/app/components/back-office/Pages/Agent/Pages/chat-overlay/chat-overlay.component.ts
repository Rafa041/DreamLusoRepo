import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { Chat, ChatMessage, ChatStatus, MessageType } from '../../../../../../models/Chat';
import { Router } from '@angular/router';
import { ChatService } from '../../../../../../services/ChatService/chat.service';
import { finalize, Subject, takeUntil } from 'rxjs';

@Component({
  selector: 'app-chat-overlay',
  templateUrl: './chat-overlay.component.html',
  styleUrl: './chat-overlay.component.scss'
})
export class ChatOverlayComponent implements OnInit {
  ngOnInit(): void {
    throw new Error('Method not implemented.');
  }

}

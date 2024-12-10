// src/app/components/session/session-list/session-list.component.ts
import { Component, OnInit } from '@angular/core';
import { SessionService } from '../../../services/session.service';
import { Session } from '../../../models/session.model';
import {CommonModule} from '@angular/common';

@Component({
  selector: 'app-session-list',
  templateUrl: './session-list.component.html',
  styleUrls: ['./session-list.component.css'],
  standalone: true,
  imports: [CommonModule]
})
export class SessionListComponent implements OnInit {
  sessions: Session[] = [];

  constructor(private sessionService: SessionService) {}

  ngOnInit(): void {
    this.loadSessions();
  }

  loadSessions(): void {
    this.sessionService.getSessions().subscribe((data) => {
      this.sessions = data;
    });
  }
}

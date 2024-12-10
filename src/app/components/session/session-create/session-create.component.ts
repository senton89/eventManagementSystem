// src/app/components/session/session-create/session-create.component.ts
import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { SessionService } from '../../../services/session.service';
import { Session } from '../../../models/session.model';
import {CommonModule} from '@angular/common';

@Component({
  selector: 'app-session-create',
  templateUrl: './session-create.component.html',
  styleUrls: ['./session-create.component.css'],
  standalone: true,
  imports: [CommonModule]
})
export class SessionCreateComponent {
  session: Session = {
    id: '',
    createdAt: new Date(),
    updatedAt: new Date(),
  };

  constructor(private sessionService: SessionService, private router: Router) {}

  createSession(): void {
    this.sessionService.createSession(this.session).subscribe(() => {
      this.router.navigate(['/sessions']);
    });
  }
}

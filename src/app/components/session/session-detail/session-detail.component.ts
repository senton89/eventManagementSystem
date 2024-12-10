// src/app/components/session/session-detail/session-detail.component.ts
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { SessionService } from '../../../services/session.service';
import { Session } from '../../../models/session.model';
import {CommonModule} from '@angular/common';
import {dateTimestampProvider} from 'rxjs/internal/scheduler/dateTimestampProvider';

@Component({
  selector: 'app-session-detail',
  templateUrl: './session-detail.component.html',
  styleUrls: ['./session-detail.component.css'],
  standalone: true,
  imports: [CommonModule]
})
export class SessionDetailComponent implements OnInit {
  session!: Session;

  constructor(private route: ActivatedRoute, private sessionService: SessionService) {
  }

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('id')!;
    this.sessionService.getSession(id).subscribe((data) => {
      this.session = data;
    });
  }
}

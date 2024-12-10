// src/app/components/notification/notification-create/notification-create.component.ts
import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { NotificationService } from '../../../services/notification.service';
import { Notification } from '../../../models/notification.model';
import {CommonModule} from '@angular/common';

@Component({
  selector: 'app-not ification-create',
  templateUrl: './notification-create.component.html',
  styleUrls: ['./notification-create.component.css'],
  standalone: true,
  imports: [CommonModule]
})
export class NotificationCreateComponent {
  notification: Notification = {
    id: '',
    createdAt: new Date(),
    updatedAt: new Date(),
  };

  constructor(private notificationService: NotificationService, private router: Router) {}

  createNotification(): void {
    this.notificationService.createNotification(this.notification).subscribe(() => {
      this.router.navigate(['/notifications']);
    });
  }
}

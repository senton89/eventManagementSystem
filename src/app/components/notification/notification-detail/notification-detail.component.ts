// src/app/components/notification/notification-detail/notification-detail.component.ts
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { NotificationService } from '../../../services/notification.service';
import { Notification } from '../../../models/notification.model';
import {CommonModule} from '@angular/common';

@Component({
  selector: 'app-notification-detail',
  templateUrl: './notification-detail.component.html',
  styleUrls: ['./notification-detail.component.css'],
  standalone: true,
  imports: [CommonModule]
})
export class NotificationDetailComponent implements OnInit {
  notification!: Notification;

  constructor(private route: ActivatedRoute, private notificationService: NotificationService) {}

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('id')!;
    this.notificationService.getNotification(id).subscribe((data) => {
      this.notification = data;
    });
  }
}

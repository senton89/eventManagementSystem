// src/app/components/notification/notification-list/notification-list.component.ts
import { Component, OnInit } from '@angular/core';
import { NotificationService } from '../../../services/notification.service';
import { Notification } from '../../../models/notification.model';
import {CommonModule} from '@angular/common';

@Component({
  selector: 'app-notification-list',
  templateUrl: './notification-list.component.html',
  styleUrls: ['./notification-list.component.css'],
  standalone: true,
  imports: [CommonModule]
})
export class NotificationListComponent implements OnInit {
  notifications: Notification[] = [];

  constructor(private notificationService: NotificationService) {}

  ngOnInit(): void {
    this.loadNotifications();
  }

  loadNotifications(): void {
    this.notificationService.getNotifications().subscribe((data) => {
      this.notifications = data;
    });
  }
}

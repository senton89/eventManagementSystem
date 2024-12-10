// src/app/models/notification.model.ts
export interface Notification {
  createdAt: Date;
  dateSent?: Date;
  event?: string;
  id: string;
  message?: string;
  updatedAt: Date;
  user?: string;
}

// src/app/models/event.model.ts
export interface Event {
  createdAt: Date;
  date?: Date;
  description?: string;
  feedbacks?: string[];
  id: string;
  location?: string;
  notifications?: string[];
  participantRegistrations?: string[];
  sessions?: string[];
  time?: Date;
  title?: string;
  updatedAt: Date;
}

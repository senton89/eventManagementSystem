// src/app/models/session.model.ts
export interface Session {
  createdAt: Date;
  endTime?: Date;
  event?: string;
  id: string;
  location?: string;
  startTime?: Date;
  title?: string;
  updatedAt: Date;
}

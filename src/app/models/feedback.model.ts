// src/app/models/feedback.model.ts
export interface Feedback {
  comment?: string;
  createdAt: Date;
  event?: string;
  id: string;
  rating?: number;
  updatedAt: Date;
  user?: string;
}

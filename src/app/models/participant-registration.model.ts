// src/app/models/participant-registration.model.ts
import { StatusEnum } from '../enums/status.enum';

export interface ParticipantRegistration {
  createdAt: Date;
  event?: string;
  id: string;
  status?: StatusEnum;
  updatedAt: Date;
  user?: string;
}

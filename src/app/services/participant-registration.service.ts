// src/app/services/participant-registration.service.ts
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ParticipantRegistration } from '../models/participant-registration.model';

@Injectable({
  providedIn: 'root'
})
export class ParticipantRegistrationService {
  private apiUrl = 'http://localhost:5202/api/participantRegistrations';

  constructor(private http: HttpClient) {}

  getParticipantRegistrations(): Observable<ParticipantRegistration[]> {
    return this.http.get<ParticipantRegistration[]>(this.apiUrl);
  }

  getParticipantRegistration(id: string): Observable<ParticipantRegistration> {
    return this.http.get<ParticipantRegistration>(`${this.apiUrl}/${id}`);
  }

  createParticipantRegistration(participantRegistration: ParticipantRegistration): Observable<ParticipantRegistration> {
    return this.http.post<ParticipantRegistration>(this.apiUrl, participantRegistration);
  }

  updateParticipantRegistration(id: string, participantRegistration: ParticipantRegistration): Observable<void> {
    return this.http.patch<void>(`${this.apiUrl}/${id}`, participantRegistration);
  }

  deleteParticipantRegistration(id: string): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }
}

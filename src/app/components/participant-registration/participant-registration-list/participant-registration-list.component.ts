// src/app/components/participant-registration/participant-registration-list/participant-registration-list.component.ts
import { Component, OnInit } from '@angular/core';
import { ParticipantRegistrationService } from '../../../services/participant-registration.service';
import { ParticipantRegistration } from '../../../models/participant-registration.model';
import {CommonModule} from '@angular/common';

@Component({
  selector: 'app-participant-registration-list',
  templateUrl: './participant-registration-list.component.html',
  styleUrls: ['./participant-registration-list.component.css'],
  standalone: true,
  imports: [CommonModule]
})
export class ParticipantRegistrationListComponent implements OnInit {
  registrations: ParticipantRegistration[] = [];

  constructor(private registrationService: ParticipantRegistrationService) {}

  ngOnInit(): void {
    this.loadRegistrations();
  }

  loadRegistrations(): void {
    this.registrationService.getParticipantRegistrations().subscribe((data) => {
      this.registrations = data;
    });
  }
}

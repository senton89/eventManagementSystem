// src/app/components/participant-registration/participant-registration-create/participant-registration-create.component.ts
import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { ParticipantRegistrationService } from '../../../services/participant-registration.service';
import { ParticipantRegistration } from '../../../models/participant-registration.model';
import {CommonModule} from '@angular/common';

@Component({
  selector: 'app-participant-registration-create',
  templateUrl: './participant-registration-create.component.html',
  styleUrls: ['./participant-registration-create.component.css'],
  standalone: true,
  imports: [CommonModule]
})
export class ParticipantRegistrationCreateComponent {
  registration: ParticipantRegistration = {
    id: '',
    createdAt: new Date(),
    updatedAt: new Date(),
  };

  constructor(private registrationService: ParticipantRegistrationService, private router: Router) {}

  createRegistration(): void {
    this.registrationService.createParticipantRegistration(this.registration).subscribe(() => {
      this.router.navigate(['/participant-registrations']);
    });
  }
}

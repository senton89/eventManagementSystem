// src/app/components/participant-registration/participant-registration-detail/participant-registration-detail.component.ts
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ParticipantRegistrationService } from '../../../services/participant-registration.service';
import { ParticipantRegistration } from '../../../models/participant-registration.model';
import {CommonModule} from '@angular/common';

@Component({
  selector: 'app-participant-registration-detail',
  templateUrl: './participant-registration-detail.component.html',
  styleUrls: ['./participant-registration-detail.component.css'],
  standalone: true,
  imports: [CommonModule]
})
export class ParticipantRegistrationDetailComponent implements OnInit {
  registration!: ParticipantRegistration;

  constructor(private route: ActivatedRoute, private registrationService: ParticipantRegistrationService) {}

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('id')!;
    this.registrationService.getParticipantRegistration(id).subscribe((data) => {
      this.registration = data;
    });
  }
}

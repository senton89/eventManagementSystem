// src/app/components/event/event-update/event-update.component.ts
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { EventService } from '../../../services/event.service';
import { Event } from '../../../models/event.model';

@Component({
  selector: 'app-event-update',
  templateUrl: './event-update.component.html',
  styleUrls: ['./event-update.component.css'],
  standalone: true,
  imports: [CommonModule, FormsModule]
})
export class EventUpdateComponent implements OnInit {
  event!: Event;

  constructor(private route: ActivatedRoute, private eventService: EventService, private router: Router) {}

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('id')!;
    this.eventService.getEvent(id).subscribe((data) => {
      this.event = data;
    });
  }

  updateEvent(): void {
    this.eventService.updateEvent(this.event.id, this.event).subscribe(() => {
      this.router.navigate(['/events']);
    });
  }
}

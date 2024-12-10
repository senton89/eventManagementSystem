// src/app/components/event/event-create/event-create.component.ts
import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { EventService } from '../../../services/event.service';
import { Event } from '../../../models/event.model';


@Component({
  selector: 'app-event-create',
  templateUrl: './event-create.component.html',
  styleUrls: ['./event-create.component.css'],
  standalone: true, // Убедитесь, что компонент является standalone
  imports: [FormsModule] // Добавляем FormsModule в imports
})
export class EventCreateComponent {
  event: Event = {
    id: '',
    createdAt: new Date(),
    updatedAt: new Date(),
  };

  constructor(private eventService: EventService, private router: Router) {}

  createEvent(): void {
    this.eventService.createEvent(this.event).
    subscribe(() => {
      this.router.navigate(['/events']);
    });
  }
}

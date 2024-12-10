// src/app/components/feedback/feedback-create/feedback-create.component.ts
import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { FeedbackService } from '../../../services/feedback.service';
import { Feedback } from '../../../models/feedback.model';
import {CommonModule} from '@angular/common';

@Component({
  selector: 'app-feedback-create',
  templateUrl: './feedback-create.component.html',
  styleUrls: ['./feedback-create.component.css'],
  standalone: true,
  imports: [CommonModule]
})
export class FeedbackCreateComponent {
  feedback: Feedback = {
    id: '',
    createdAt: new Date(),
    updatedAt: new Date(),
  };

  constructor(private feedbackService: FeedbackService, private router: Router) {}

  createFeedback(): void {
    this.feedbackService.createFeedback(this.feedback).subscribe(() => {
      this.router.navigate(['/feedbacks']);
    });
  }
}

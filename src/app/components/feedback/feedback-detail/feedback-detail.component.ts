// src/app/components/feedback/feedback-detail/feedback-detail.component.ts
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { FeedbackService } from '../../../services/feedback.service';
import { Feedback } from '../../../models/feedback.model';
import {CommonModule} from '@angular/common';

@Component({
  selector: 'app-feedback-detail',
  templateUrl: './feedback-detail.component.html',
  styleUrls: ['./feedback-detail.component.css'],
  standalone: true,
  imports: [CommonModule]
})
export class FeedbackDetailComponent implements OnInit {
  feedback!: Feedback;

  constructor(private route: ActivatedRoute, private feedbackService: FeedbackService) {}

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('id')!;
    this.feedbackService.getFeedback(id).subscribe((data) => {
      this.feedback = data;
    });
  }
}

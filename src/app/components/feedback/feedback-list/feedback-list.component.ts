// src/app/components/feedback/feedback-list/feedback-list.component.ts
import { Component, OnInit } from '@angular/core';
import { FeedbackService } from '../../../services/feedback.service';
import { Feedback } from '../../../models/feedback.model';
import {CommonModule} from '@angular/common';

@Component({
  selector: 'app-feedback-list',
  templateUrl: './feedback-list.component.html',
  styleUrls: ['./feedback-list.component.css'],
  standalone: true,
  imports: [CommonModule]
})
export class FeedbackListComponent implements OnInit {
  feedbacks: Feedback[] = [];

  constructor(private feedbackService: FeedbackService) {}

  ngOnInit(): void {
    this.loadFeedbacks();
  }

  loadFeedbacks(): void {
    this.feedbackService.getFeedbacks().subscribe((data) => {
      this.feedbacks = data;
    });
  }
}

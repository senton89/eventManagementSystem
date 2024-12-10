// src/app/app-routing.module.ts
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { EventListComponent } from './components/event/event-list/event-list.component';
import { EventDetailComponent } from './components/event/event-detail/event-detail.component';
import { EventCreateComponent } from './components/event/event-create/event-create.component';
import { EventUpdateComponent } from './components/event/event-update/event-update.component';
import { FeedbackListComponent } from './components/feedback/feedback-list/feedback-list.component';
import { FeedbackDetailComponent } from './components/feedback/feedback-detail/feedback-detail.component';
import { FeedbackCreateComponent } from './components/feedback/feedback-create/feedback-create.component';
import { NotificationListComponent } from './components/notification/notification-list/notification-list.component';
import { NotificationDetailComponent } from './components/notification/notification-detail/notification-detail.component';
import { NotificationCreateComponent } from './components/notification/notification-create/notification-create.component';
import { ParticipantRegistrationListComponent } from './components/participant-registration/participant-registration-list/participant-registration-list.component';
import { ParticipantRegistrationDetailComponent } from './components/participant-registration/participant-registration-detail/participant-registration-detail.component';
import { ParticipantRegistrationCreateComponent } from './components/participant-registration/participant-registration-create/participant-registration-create.component';
import { SessionListComponent } from './components/session/session-list/session-list.component';
import { SessionDetailComponent } from './components/session/session-detail/session-detail.component';
import { SessionCreateComponent } from './components/session/session-create/session-create.component';

const routes: Routes = [
  { path: 'events', component: EventListComponent },
  { path: 'events/create', component: EventCreateComponent },
  { path: 'events/:id', component: EventDetailComponent },
  { path: 'events/:id/edit', component: EventUpdateComponent },
  { path: 'feedbacks', component: FeedbackListComponent },
  { path: 'feedbacks/create', component: FeedbackCreateComponent },
  { path: 'feedbacks/:id', component: FeedbackDetailComponent },
  { path: 'notifications', component: NotificationListComponent },
  { path: 'notifications/create', component: NotificationCreateComponent },
  { path: 'notifications/:id', component: NotificationDetailComponent },
  { path: 'participant-registrations', component: ParticipantRegistrationListComponent },
  { path: 'participant-registrations/create', component: ParticipantRegistrationCreateComponent },
  { path: 'participant-registrations/:id', component: ParticipantRegistrationDetailComponent },
  { path: 'sessions', component: SessionListComponent },
  { path: 'sessions/create', component: SessionCreateComponent },
  { path: 'sessions/:id', component: SessionDetailComponent },
  { path: '', redirectTo: '/events', pathMatch: 'full' },
  { path: '**', redirectTo: '/events' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {}

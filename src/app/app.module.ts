// src/app/app.module.ts
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { AppRoutingModule } from './app.routes';
import { AppComponent } from './app.component';
import { EventListComponent } from './components/event/event-list/event-list.component';
import { EventDetailComponent } from './components/event/event-detail/event-detail.component';
import { EventCreateComponent } from './components/event/event-create/event-create.component';
import { EventUpdateComponent } from './components/event/event-update/event-update.component';
// Импортируйте другие компоненты аналогично

@NgModule({
  declarations: [
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    HttpClientModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule {}

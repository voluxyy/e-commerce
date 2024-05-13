import { ApplicationConfig, NgModule } from '@angular/core';
import { provideRouter } from '@angular/router';

import { routes } from './app.routes';

NgModule({
  providers: [provideRouter(routes)]
})

export const appConfig: ApplicationConfig = {
  providers: [provideRouter(routes)]
};

import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AdminComponent } from './pages/admin/admin.component';
import { FlightFindComponent } from './pages/admin/flight-find/flight-find.component';
import { FlightAddComponent } from './pages/admin/flight-add/flight-add.component';
import { FlightSearchComponent } from './pages/public/flight-search/flight-search.component';
import { PublicComponent } from './pages/public/public.component';
import { PublicGuard } from './shared/guards/public.guard';
import { AdminGuard } from './shared/guards/admin.guard';

const routes: Routes = [
  {
    path: '',
    redirectTo: 'flights',
    pathMatch: 'full'
  },

  {
    path: '',
    component: PublicComponent,
    canActivate: [PublicGuard],
    children: [
      {
        path: 'flights',
        children: [
          {
            path: '',
            redirectTo: 'search',
            pathMatch: 'full'
          },

          {
            path: 'search',
            component: FlightSearchComponent
          }
        ]
      }
    ]
  },

  {
    path: '',
    component: AdminComponent,
    canActivate: [AdminGuard],
    children: [
      {
        path: 'admin',
        pathMatch: 'full',
        redirectTo: 'flights'
      },

      {
        path: 'flights',
        children: [
          {
            path: '',
            redirectTo: 'find',
            pathMatch: 'full'
          },

          {
            path: 'find',
            component: FlightFindComponent

          },

          {
            path: 'add',
            component: FlightAddComponent
          }
        ]
      }
    ]
  }

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }

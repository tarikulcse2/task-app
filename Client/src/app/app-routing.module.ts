import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LoginComponent } from './components/login/login.component';
import { LayoutComponent } from './components/layout/layout.component';
import { RegistrationComponent } from './components/registration/registration.component';
import { HomeComponent } from './components/home/home.component';
import { JobComponent } from './components/job/job.component';
import { AuthGuard } from './services/authgurd.service';
const routes: Routes = [
  {
    path: '',
    redirectTo: 'login',
    pathMatch: 'full'
  },
  {
    path: 'login',
    component: LoginComponent
  },
  {
    path: 'reg',
    component: RegistrationComponent
  },
  {
    path: 'home',
    component: LayoutComponent,
    canActivate: [AuthGuard],
    children: [
    {
      path: '',
      component: HomeComponent
    },
    {
      path: 'job/edit/:id',
      component: JobComponent
    },
    {
      path: 'job',
      component: JobComponent
    }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }

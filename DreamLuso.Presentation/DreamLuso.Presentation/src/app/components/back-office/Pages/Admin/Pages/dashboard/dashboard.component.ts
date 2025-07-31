import { Component, OnInit } from '@angular/core';
import { UserModel } from '../../../../../../models/UserModel';
import { UserService } from '../../../../../../services/UserService/user.service';
import { Router } from '@angular/router';
import { environment } from '../../../../../../../../environment';
import { PropertyService } from '../../../../../../services/PropertyService/property.service';
import { forkJoin } from 'rxjs/internal/observable/forkJoin';
import { PropertyStatus } from '../../../../../../models/property';
import { Access } from '../../../../../../models/Access';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrl: './dashboard.component.scss'
})
export class DashboardAdminComponent implements OnInit {
  loggedUserDetails: UserModel | null = null;
  totalUsers: number = 0;
  totalProperties: number = 0;
  totalAgents: number = 0;
  totalRevenue: number = 0;
  pendingProperties: any[] = [];
  pendingApprovals: number = 0;

  recentActivities = [
    { type: 'user', action: 'New user registration', time: '5 minutes ago' },
    { type: 'property', action: 'Property listing approved', time: '15 minutes ago' },
    { type: 'agent', action: 'New agent verification request', time: '1 hour ago' }
  ];

  routes: { [key: string]: string } = {
    'addAgent': '/back-office/admin/create-agent',
    'newProperty': '/back-office/agent/create-property',
    'generateReport': '/back-office/admin/reports',
    'settings': '/back-office/admin/settings'
  };

  constructor(
    private userService: UserService,
    private propertyService: PropertyService,
    private router: Router
  ) {}


  navigateToAction(route: string): void {
    this.router.navigate([this.routes[route]]);
  }

  ngOnInit(): void {
    this.loadDashboardData();
  }
  private loadDashboardData(): void {
    forkJoin({
      users: this.userService.getAllUsers(),
      properties: this.propertyService.retrieveAll()
    }).subscribe({
      next: (data) => {
        this.totalUsers = data.users.length;
        this.totalAgents = data.users.filter(user => user.access === Access.RealEstateAgent).length;
        this.totalProperties = data.properties.filter(prop => prop.isActive).length;

        // Get pending properties (isActive = false)
        this.pendingProperties = data.properties.filter(prop => !prop.isActive);
        this.pendingApprovals = this.pendingProperties.length;

        this.totalRevenue = data.properties
          .filter(prop => prop.status === PropertyStatus.Sold)
          .reduce((sum, prop) => sum + prop.price, 0);
      },
      error: (error) => {
        console.error('Erro ao carregar dados do dashboard:', error);
      }
    });
  }
  viewProperty(propertyId: string): void {
    this.router.navigate(['/propertyId/', propertyId]);
  }
  approveProperty(propertyId: string): void {
    this.propertyService.updatePropertyIsActive(propertyId, true).subscribe({
      next: () => {
        this.loadDashboardData();
      },
      error: (error) => {
        console.error('Erro ao aprovar propriedade:', error);
      }
    });
  }
}

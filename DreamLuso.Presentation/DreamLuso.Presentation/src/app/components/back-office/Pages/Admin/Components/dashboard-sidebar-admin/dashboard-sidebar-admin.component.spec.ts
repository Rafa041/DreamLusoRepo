import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DashboardSidebarAdminComponent } from './dashboard-sidebar-admin.component';

describe('DashboardSidebarAdminComponent', () => {
  let component: DashboardSidebarAdminComponent;
  let fixture: ComponentFixture<DashboardSidebarAdminComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [DashboardSidebarAdminComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DashboardSidebarAdminComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

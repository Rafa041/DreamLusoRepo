import { TestBed } from '@angular/core/testing';

import { PropertyVisitService } from './property-visit.service';

describe('PropertyVisitService', () => {
  let service: PropertyVisitService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(PropertyVisitService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});

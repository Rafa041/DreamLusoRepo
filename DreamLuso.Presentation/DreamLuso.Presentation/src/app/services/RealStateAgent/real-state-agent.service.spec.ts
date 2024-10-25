import { TestBed } from '@angular/core/testing';

import { RealStateAgentService } from './real-state-agent.service';

describe('RealStateAgentService', () => {
  let service: RealStateAgentService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(RealStateAgentService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});

import { TestBed } from '@angular/core/testing';

import { AuthLayout } from './auth-layout-service';

describe('AuthLayout', () => {
  let service: AuthLayout;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(AuthLayout);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});

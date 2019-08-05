import { TestBed } from '@angular/core/testing';

import { FormOptionsService } from './form-options.service';

describe('FormOptionsService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: FormOptionsService = TestBed.get(FormOptionsService);
    expect(service).toBeTruthy();
  });
});

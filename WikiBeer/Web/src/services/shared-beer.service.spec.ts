import { TestBed } from '@angular/core/testing';

import { SharedBeerService } from './shared-beer.service';

describe('SharedBeerService', () => {
  let service: SharedBeerService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(SharedBeerService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});

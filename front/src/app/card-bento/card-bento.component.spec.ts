import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CardBentoComponent } from './card-bento.component';

describe('CardBentoComponent', () => {
  let component: CardBentoComponent;
  let fixture: ComponentFixture<CardBentoComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CardBentoComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(CardBentoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

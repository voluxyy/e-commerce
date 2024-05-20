import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EditMemberComponent } from './edit-member.component';

describe('EditMemberComponent', () => {
  let component: EditMemberComponent;
  let fixture: ComponentFixture<EditMemberComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [EditMemberComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(EditMemberComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

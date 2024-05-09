import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MembersAdminComponent } from './members-admin.component';

describe('MembersAdminComponent', () => {
  let component: MembersAdminComponent;
  let fixture: ComponentFixture<MembersAdminComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [MembersAdminComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(MembersAdminComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

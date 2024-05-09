// import { Component } from '@angular/core';
// import { FormBuilder, FormGroup, Validators } from '@angular/forms';
// import { UserService, User } from '../user.service';

// @Component({
//   selector: 'app-user-form',
//   templateUrl: './user-form.component.html',
//   styleUrls: ['./user-form.component.css']
// })
// export class UserFormComponent {
//   userForm: FormGroup;

//   constructor(private formBuilder: FormBuilder, private userService: UserService) {
//     this.userForm = this.formBuilder.group({
//       email: ['', [Validators.required, Validators.email]],
//       password: ['', [Validators.required, Validators.minLength(6)]]
//     });
//   }

//   onSubmit() {
//     if (this.userForm.valid) {
//       const user: User = {
//         email: this.userForm.value.email,
//         password: this.userForm.value.password
//       };
//       this.userService.addUser(user).subscribe(
//         (response) => {
//           console.log('User added successfully:', response);
//           // Afficher un message de succès à l'utilisateur ou rediriger vers une autre page, etc.
//         },
//         (error) => {
//           console.error('Error adding user:', error);
//           // Afficher un message d'erreur à l'utilisateur
//         }
//       );
//     }
//   }
// }

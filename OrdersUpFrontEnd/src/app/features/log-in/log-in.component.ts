import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { User } from 'src/app/models/user';
import { UsersService } from 'src/app/services/users.service';

@Component({
  selector: 'app-log-in',
  templateUrl: './log-in.component.html',
  styleUrls: ['./log-in.component.css']
})
export class LogInComponent implements OnInit {
  user !: User;
  show = false;
  password;

  constructor(private _userService: UsersService, private router: Router, private toastr: ToastrService) { 
    this.user = new User();
  }

  ngOnInit(): void {
    this.password = 'password';
  }

  showPassword(){
    if (this.password === 'password') {
      this.password = 'text';
      this.show = true;
    } else {
      this.password = 'password';
      this.show = false;
    }
  }

  onSubmit(){
    this._userService.login(this.user).subscribe(
      (res:any)=>{
        localStorage.setItem('token', res.token);
        this.router.navigate(['/inventory']);
      },
      err => {
        if(err.status == 400){
          this.toastr.error('Nombre de usuario o contraseña incorrecta.', 'Acceso Invalido');
        }else{
          console.log(err);
        }
      }
    )
  }
}

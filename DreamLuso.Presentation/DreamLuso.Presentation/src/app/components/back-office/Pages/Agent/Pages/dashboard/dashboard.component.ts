import { Component } from '@angular/core';
import { UserModel } from '../../../../../../models/UserModel';
import { UserService } from '../../../../../../services/UserService/user.service';
import { Router } from '@angular/router';
import { environment } from '../../../../../../../../environment';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrl: './dashboard.component.scss'
})
export class DashboardAgentComponent {
  loggedUserDetails: UserModel | null = null;
  userId: string = "";
  errorMessage: string = "";
  isDropdownOpen: boolean = false; // Controle do dropdown

  constructor(
    private userService: UserService,
    private router: Router
  ) {}

  ngOnInit() {
    const loggedUserString = sessionStorage.getItem('loggedUser');

    if (loggedUserString) {
      const loggedUser = JSON.parse(loggedUserString);
      this.userId = loggedUser.id;

      this.userService.retrieve(this.userId).subscribe({
        next: (userDetails: UserModel) => {
          this.loggedUserDetails = userDetails;
          console.log('Logged user details:', this.loggedUserDetails);
        },
        error: (error) => {
          console.error('Error fetching user details:', error);
          this.errorMessage = 'Failed to load user details';
        },
      });
    } else {
      // Se não houver usuário logado, redirecione para a página de login ou exiba uma mensagem de erro
      this.router.navigate(['/login']);
    }

    document.addEventListener('click', this.closeDropdown.bind(this));
  }

  ngOnDestroy() {
    document.removeEventListener('click', this.closeDropdown.bind(this)); // Remove o listener ao destruir o componente
  }

  getImageUrl(imageUrl: string | undefined): string {
    if (!imageUrl) {
      return 'assets/default-avatar.jpg';  // Caminho para a imagem padrão
    }
    return `${environment.apiUrl}${imageUrl}`;
  }

  handleImageError(event: any) {
    event.target.src = 'assets/default-avatar.jpg';  // Caminho para a imagem padrão em caso de erro
  }
  logout() {
    // Remover o usuário do sessionStorage
    sessionStorage.removeItem('loggedUser');

    // Redirecionar para a página de login
    this.router.navigate(['']);
  }
  toggleDropdown() {
    this.isDropdownOpen = !this.isDropdownOpen; // Alterna o estado do dropdown
  }
    // Método para fechar o dropdown ao clicar fora dele
  closeDropdown(event: MouseEvent) {
     const target = event.target as HTMLElement;
    if (!target.closest('.relative')) {
      this.isDropdownOpen = false;
    }
  }

}

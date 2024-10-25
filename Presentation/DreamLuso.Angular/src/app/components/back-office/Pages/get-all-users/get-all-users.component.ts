import { Component, OnInit } from '@angular/core';
import { RetrieveUserResponse } from '../../../../models/RetrieveAllUsers';
import { UserService } from '../../../../services/user.service';
import { environment } from '../../../../../../enviroment.prod';
import { Access } from '../../../../models/Access';

@Component({
  selector: 'app-get-all-users',
  templateUrl: './get-all-users.component.html',
  styleUrl: './get-all-users.component.scss'
})
export class GetAllUsersComponent implements OnInit {
  users: RetrieveUserResponse[] = [];
  errorMessage: string | null = null;
  private apiUrl = environment.apiUrl; // Assumindo que você tem a URL da API no seu environment

  constructor(private userService: UserService) {}

  ngOnInit(): void {
    this.loadUsers();
  }

  loadUsers(): void {
    this.userService.getAllUsers().subscribe({
      next: (response) => {
        console.log('Resposta do servidor:', response);
        if (Array.isArray(response)) {
          this.users = this.mapUserAccess(response);
        } else if (response && Array.isArray(response.users)) {
          this.users = this.mapUserAccess(response.users);
        } else {
          this.errorMessage = 'Formato de resposta inválido.';
        }
      },
      error: (error) => {
        console.error('Erro ao carregar usuários:', error);
        this.errorMessage = 'Erro ao carregar a lista de usuários.';
      }
    });
  }

  // Função para mapear os valores de access recebidos do servidor para o enum
  mapUserAccess(users: RetrieveUserResponse[]): RetrieveUserResponse[] {
    return users.map(user => ({
      ...user,
      access: this.mapAccessValue(user.access)
    }));
  }

  // Mapeia o valor do access para o enum correspondente, corrigindo o formato de "RealStateAgent"
  mapAccessValue(access: string): Access {
    switch (access) {
      case 'None':
        return Access.None;
      case 'Guest':
        return Access.Guest;
      case 'User':
        return Access.User;
      case 'RealStateAgent':
        return Access.RealStateAgent; // Ajusta o formato
      case 'Admin':
        return Access.Admin;
      case 'Blocked':
        return Access.Blocked;
      default:
        return Access.None; // Retorna um valor padrão caso o valor seja inválido
    }
  }

  getImageUrl(imageUrl: string | undefined): string {
    if (!imageUrl) {
      return 'https://avatar.iran.liara.run/public';
    }
    // Usando a URL base do environment + o caminho da imagem
    return `${this.apiUrl}${imageUrl}`;
  }

  handleImageError(event: any) {
    event.target.src = 'https://avatar.iran.liara.run/public';
  }

}

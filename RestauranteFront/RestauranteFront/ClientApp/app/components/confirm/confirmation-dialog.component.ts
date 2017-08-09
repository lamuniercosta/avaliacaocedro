import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from "@angular/router";
import { RestauranteService } from '../restaurante/restaurante.model';
import { PratoService } from '../prato/prato.model';

@Component({
    selector: 'app-confirmation-dialog',
    templateUrl: 'confirmation-dialog.component.html'
})
export class ConfirmationDialogComponent implements OnInit {
    constructor(private restauranteService: RestauranteService, private pratoService: PratoService, private route: ActivatedRoute, private router: Router) { }

    model: ConfirmationModel;
    id: number;
    caller: string;
    message: string;

    ngOnInit() {
        this.id = this.route.snapshot.params['id'];
        this.caller = this.route.snapshot.params['caller'];
    }

    excluirRegistro() {
        if (this.caller === 'restaurante') {
            this.restauranteService.DeleteRestaurante(this.id).subscribe((response) => {
                if (response.status == 200) {
                    this.goBack()
                }
                else {
                    this.message = '<div class="alert alert-danger" role="alert">' + response.text() + '</div>';
                }
            });
        }
        else if (this.caller === 'prato') {
            this.pratoService.DeletePrato(this.id).subscribe((response) => {
                if (response.status == 200) {
                    this.goBack()
                }
                else {
                    this.message = '<div class="alert alert-danger" role="alert">' + response.text() + '</div>';
                }
            });
        }
    }

    goBack(): void {
        this.router.navigate(['/'+this.caller]);
    }

}

export interface IConfirmation {
    id: number;
    nome: string;
    caller: string;
}

export class ConfirmationModel {
    constructor(
        id: number,
        nome: string,
        caller: string
    ) { }
}
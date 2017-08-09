import { Component, OnInit } from '@angular/core';
import { Http } from '@angular/http';
import { RestauranteService, IRestaurante, RestauranteModel } from './restaurante.model';
import { Location } from '@angular/common';
import { ActivatedRoute, ParamMap, Router } from '@angular/router';

@Component({
    selector: "editRestaurante",
    templateUrl: 'edit.restaurante.component.html',
    providers: [RestauranteService]
})

export class EditRestaurante implements OnInit {
    model: RestauranteModel;
    id: number;
    message: string;
    constructor(private service: RestauranteService, private route: ActivatedRoute, private router: Router)
    { }


    ngOnInit(): void {
        this.id = this.route.snapshot.params['id'];
        
        if (this.id > 0) {
            this.model = this.service.GetRestaurante(this.id).then(model=> this.model = model);
        }
        else {
            this.model = new RestauranteModel(0,'');
        }
        
    }

    SaveRestaurante() {
        this.service.SaveRestaurante(this.id, this.model).subscribe((response) => {
            if (response.status == 200) {
                this.goBack()
            }
            else {
                this.message = '<div class="alert alert-danger" role="alert">' + response.text()+'</div>';
            }
           });
    }

    goBack(): void {
        this.router.navigate(['/restaurante']);
    }
}
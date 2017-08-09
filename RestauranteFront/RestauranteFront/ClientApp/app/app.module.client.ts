import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { sharedConfig } from './app.module.shared';
import { RestauranteService } from './components/restaurante/restaurante.model';
import { PratoService } from './components/prato/prato.model';

@NgModule({
    bootstrap: sharedConfig.bootstrap,
    declarations: [sharedConfig.declarations],
    imports: [
        BrowserModule,
        FormsModule,
        HttpModule,
        ...sharedConfig.imports

    ],
    providers: [
        { provide: 'ORIGIN_URL', useValue: location.origin },
        RestauranteService,
        PratoService
    ]
})
export class AppModule {
}

import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { NgxEchartsModule } from 'ngx-echarts';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { AuthInterceptor } from './core/auth/auth.interceptor';

@NgModule({
	declarations: [AppComponent],
	imports: [
		BrowserModule,
		AppRoutingModule,
		HttpClientModule,
		NgxEchartsModule.forRoot({
			echarts: () => import('echarts'),
		}),
	],
	providers: [{ provide: HTTP_INTERCEPTORS, useClass: AuthInterceptor, multi: true }],
	bootstrap: [AppComponent],
})
export class AppModule {}

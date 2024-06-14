import { Environment } from '@abp/ng.core';

const baseUrl = 'http://localhost:4200';

export const environment = {
  production: false,
  application: {
    baseUrl: 'http://localhost:4200/',
    name: 'NnLibCommon',
    logoUrl: '',
  },
  oAuthConfig: {
    issuer: 'https://localhost:44364/',
    redirectUri: baseUrl,
    clientId: 'NnLibCommon_App',
    responseType: 'code',
    scope: 'offline_access NnLibCommon',
    requireHttps: true
  },
  apis: {
    default: {
      url: 'https://localhost:44364',
      rootNamespace: 'Necnat.Abp.NnLibCommon',
    },
    NnLibCommon: {
      url: 'https://localhost:44342',
      rootNamespace: 'Necnat.Abp.NnLibCommon',
    },
  },
} as Environment;

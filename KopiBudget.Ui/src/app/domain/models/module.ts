export class ModuleResponse {
  name: string;
  link: string;
}
export class ModuleFragment {
  name: string;
}

export class ModuleGroupResponse {
  key: string;
  modules: ModuleResponse[];
}

pvc.Task("default", ()=> {
pvc.Source("madhonmvc4.sln")
   .Pipe(new PvcMSBuild(
 		buildTarget: "Clean;Build",
        enableParallelism: true
   ));
  });
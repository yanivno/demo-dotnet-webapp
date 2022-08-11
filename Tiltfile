SOURCE_IMAGE = os.getenv("SOURCE_IMAGE", default='gcr.io/ynorman-project/local-builds')
LOCAL_PATH = os.getenv("LOCAL_PATH", default='.')
NAMESPACE = os.getenv("NAMESPACE", default='default')

local_resource(
      'build',
      'dotnet publish src --configuration Release --runtime ubuntu.18.04-x64 --self-contained false --output ./out',
      deps=['src'],
      ignore=['src/bin','src/obj']
)

k8s_custom_deploy(
    'demo-dotnet-webapp',
    apply_cmd="tanzu apps workload apply -f config/workload.yaml --live-update" +
               " --local-path " + LOCAL_PATH +
               " --source-image " + SOURCE_IMAGE +
               " --namespace " + NAMESPACE +
               " --yes >/dev/null" +
               " && kubectl get workload demo-dotnet-webapp --namespace " + NAMESPACE + " -o yaml",
    delete_cmd="tanzu apps workload delete -f config/workload.yaml --namespace " + NAMESPACE + " --yes",
    deps=['.'],
    container_selector='workload',
    live_update=[
      sync('out', '/app/out'),
      run('cp -rf /workspace/build/* /app', trigger='./build')
    ]
)

k8s_resource('demo-dotnet-webapp', port_forwards=["8080:80"],
            extra_pod_selectors=[{'serving.knative.dev/service': 'demo-dotnet-webapp'}])

allow_k8s_contexts(k8s_context())
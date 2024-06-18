CommandsService port: 6000

PlatformService port: 5000


executable command before ingress-srv: kubectl apply -f https://raw.githubusercontent.com/kubernetes/ingress-nginx/controller-v1.10.1/deploy/static/provider/cloud/deploy.yaml
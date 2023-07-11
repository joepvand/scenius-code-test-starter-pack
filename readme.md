# Scenius Code Test Starter Pack

## Notes

### Backend
- Messaging implementation met MassTransit, dit omdat de abstractie over je gekozen message bus erg handig is, en masstransit dit makkelijk maakt.
- RabbitMQ
- Postgres database
- Backend unit test voorbeelden
- Realtime communicatie met SignalR. 
### Frontend
- Frontend in angular
- Bootstrap geinstalleerd en gebruikt
- e2e testing met protractor geprobeerd, maar mijn 0 ervaring in Angular maakte dit nog te lastig, ik kwam niet door compile / dependency issues heen. 

### Docker / k8s

- Applicaties gedockerized met docker compose
- K8s ready gemaakt, echter theoretisch. Images staan niet op een container registry, en het cluster zou moeten worden voorzien van een ingress point / loadbalancer.
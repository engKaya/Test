# Denemelerim

#LockManagement
Bir senaryoda bir müşterinin aynı kart ile transactionda bulunması durumunda threadlerin bakiye güncelleme sırasında o an akışta olan threadi beklemesini ancak farklı müşteri transactionlarının threadlerinin engellenmemesi gerekmekte (veya aynı müşteri farklı kart). LockManager ile semaphoreslim objesi oluşturup transactin akışında kullandım. Aynı müşteri numarası ve card numarasını bir concurrendictionaryde (basit bi unique id) tutup valuesuna yeni bir Semaphore objesi ekledim. 

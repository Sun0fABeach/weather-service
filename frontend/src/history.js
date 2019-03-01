export default class History {
  constructor () {
    this.dbName = 'searchHistory'
    this.storeName = 'weatherReports'

    if (indexedDB) {
      // indexedDB.deleteDatabase(this.dbName)
      const dbRequest = indexedDB.open(this.dbName)
      dbRequest.onupgradeneeded = event => {
        this.db = event.target.result
        this.db.createObjectStore(this.storeName, { autoIncrement: true })
      }
      dbRequest.onsuccess = event => {
        this.db = event.target.result
      }
      dbRequest.onerror = event => {
        throw new Error('Can not open database')
      }
    } else {
      throw new Error('IndexedDB not supported')
    }
  }

  add (item) {
    this.getStore('readwrite').add(item)
  }

  retrieve () {
    const request = this.getStore('readonly').getAll()
    return new Promise(resolve => {
      request.onsuccess = () => { // failure ignored here
        resolve(request.result)
      }
    })
  }

  clear () {
    this.getStore('readwrite').clear()
  }

  getStore (mode) {
    return this.db
      .transaction(this.storeName, mode)
      .objectStore(this.storeName)
  }
}
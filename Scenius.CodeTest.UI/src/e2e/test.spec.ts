import { browser } from "protractor";

describe('Protractor Demo App', function() {
    it('should have a title', async function() {
      browser.get('http://localhost:4200');
  
      expect(await browser.getTitle()).toEqual('');
    });
  });
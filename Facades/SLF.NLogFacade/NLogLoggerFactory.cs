// SLF.NET - Simple Logging Fa�ade for .NET
// Contact and Information: http://slf.codeplex.com
//
// This library is free software; you can redistribute it and/or
// modify it in any way you see fit as long as this copyright
// notice is not being removed.
// 
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
// OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT
// HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
// WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
// FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
// OTHER DEALINGS IN THE SOFTWARE.
//
// THIS COPYRIGHT NOTICE MAY NOT BE REMOVED FROM THIS FILE


using System;
using System.Collections.Generic;
using System.Text;
using Slf;
using NLog;

namespace SLF.NLogFacade
{
  /// <summary>
  /// An implementation of the <see cref="ILoggerFactory"/>
  /// interface which creates <see cref="ILogger"/> instances
  /// that use the NLog framework as the underlying logging
  /// mechanism.
  /// </summary>
  public class NLogLoggerFactory : ILoggerFactory
  {  
    public ILogger GetLogger(string name)
    {
      return new NLogLogger(LogManager.GetLogger(name));
    }
  }
}

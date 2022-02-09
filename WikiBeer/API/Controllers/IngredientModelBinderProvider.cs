//using Ipme.WikiBeer.Dtos.Ingredients;
//using Microsoft.AspNetCore.Mvc.ModelBinding;
//using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

//namespace Ipme.WikiBeer.API.Controllers
//{
//    public class IngredientModelBinderProvider : IModelBinderProvider
//    {
//        public IModelBinder GetBinder(ModelBinderProviderContext context)
//        {
//            if (context.Metadata.ModelType != typeof(IngredientDto))
//            {
//                return null;
//            }

//            var subclasses = new[] { typeof(HopDto), typeof(AdditiveDto), typeof(CerealDto), };

//            var binders = new Dictionary<Type, (ModelMetadata, IModelBinder)>();
//            foreach (var type in subclasses)
//            {
//                var modelMetadata = context.MetadataProvider.GetMetadataForType(type);
//                binders[type] = (modelMetadata, context.CreateBinder(modelMetadata));
//            }

//            return new IngredientModelBinder(binders);
//        }
//    }

//    public class IngredientModelBinder : IModelBinder
//    {
//        private Dictionary<Type, (ModelMetadata, IModelBinder)> binders;

//        public IngredientModelBinder(Dictionary<Type, (ModelMetadata, IModelBinder)> binders)
//        {
//            this.binders = binders;
//        }

//        public async Task BindModelAsync(ModelBindingContext bindingContext)
//        {
//            var modelKindName = ModelNames.CreatePropertyModelName(bindingContext.ModelName, nameof(Device.Kind));
//            var modelTypeValue = bindingContext.ValueProvider.GetValue(modelKindName).FirstValue;

//            IModelBinder modelBinder;
//            ModelMetadata modelMetadata;
//            if (modelTypeValue == "Laptop")
//            {
//                (modelMetadata, modelBinder) = binders[typeof(Laptop)];
//            }
//            else if (modelTypeValue == "SmartPhone")
//            {
//                (modelMetadata, modelBinder) = binders[typeof(SmartPhone)];
//            }
//            else
//            {
//                bindingContext.Result = ModelBindingResult.Failed();
//                return;
//            }

//            var newBindingContext = DefaultModelBindingContext.CreateBindingContext(
//                bindingContext.ActionContext,
//                bindingContext.ValueProvider,
//                modelMetadata,
//                bindingInfo: null,
//                bindingContext.ModelName);

//            await modelBinder.BindModelAsync(newBindingContext);
//            bindingContext.Result = newBindingContext.Result;

//            if (newBindingContext.Result.IsModelSet)
//            {
//                // Setting the ValidationState ensures properties on derived types are correctly 
//                bindingContext.ValidationState[newBindingContext.Result.Model] = new ValidationStateEntry
//                {
//                    Metadata = modelMetadata,
//                };
//            }
//        }
//    }
//}
